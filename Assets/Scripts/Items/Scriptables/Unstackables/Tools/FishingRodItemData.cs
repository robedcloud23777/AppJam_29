using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

[CreateAssetMenu(fileName = "FishingRod Item Data", menuName = "Scriptables/Items/Unstackables/Tools/FishingRod", order = 0)]
public class FishingRodItemData : ToolItemData
{
    [Header("FishingRod")]
    [SerializeField] CooldownSource m_castCooldownSource;
    [SerializeField] float m_castCooldown;
    [SerializeField] float m_minWaitTime, m_maxWaitTime;
    [SerializeField] int m_fishExperience;
    [SerializeField] LootTable m_loot;
    public CooldownSource castCooldownSource => m_castCooldownSource;
    public float castCooldown => m_castCooldown;
    public float minWaitTime => m_minWaitTime;
    public float maxWaitTime => m_maxWaitTime;
    public int fishExperience => m_fishExperience;
    public LootTable loot => m_loot;
    public override Item Create()
    {
        return new FishingRodItem(this);
    }
}
public class FishingRodItem : ToolItem, ICooldownDisplayed, IStatDisplayed
{
    new public readonly FishingRodItemData data;
    public FishingRod fishingRod => base.tool as FishingRod;
    public FishingRodItem(FishingRodItemData data) : base(data)
    {
        this.data = data;
        topLayer = new(this, new());
    }
    public float cooldownLeft => data.castCooldownSource.cooldownLeft / data.castCooldownSource.cooldown;
    bool attacking = false;
    public override AnimationType heldAnimation => AnimationType.FishingRod;
    const float castDistX = 3.0f, castDistY = -1.0f;
    TopLayer topLayer;
    public override void OnWield(Player wielder)
    {
        base.OnWield(wielder);
        topLayer.OnStateEnter();
    }
    public override void OnWieldUpdate()
    {
        base.OnWieldUpdate();
        topLayer.OnStateUpdate();
    }
    public override void OnUnwield(Player wielder)
    {
        topLayer.OnStateExit();
        base.OnUnwield(wielder);
    }
    class FishingRodVals : FSMVals
    {

    }

    class TopLayer : TopLayer<FishingRodItem, FishingRodVals>
    {
        public TopLayer(FishingRodItem origin, FishingRodVals values) : base(origin, values)
        {
            defaultState = new Idle(origin, this);
            AddState("Idle", defaultState);
            AddState("Casting", new Casting(origin, this));
        }
        class Idle : State<FishingRodItem, FishingRodVals>
        {
            public Idle(FishingRodItem origin, Layer<FishingRodItem, FishingRodVals> parent) : base(origin, parent)
            {

            }
            public override void OnStateUpdate()
            {
                base.OnStateUpdate();
                if (Input.GetMouseButton(0) && UIScanner.ScanUI().Count <= 0 && !origin.data.castCooldownSource.isOnCooldown)
                {
                    Vector2 pos = (Vector2)origin.wielder.transform.position + Vector2.right * origin.wielder.movement.lookingDir * castDistX + Vector2.up * castDistY;
                    if (Physics2D.OverlapPoint(pos, LayerMask.GetMask("Water")))
                    {
                        parentLayer.ChangeState("Casting");
                    }
                }
            }
        }
        class Casting : Layer<FishingRodItem, FishingRodVals>
        {
            public Casting(FishingRodItem origin, Layer<FishingRodItem, FishingRodVals> parent) : base(origin, parent)
            {
                defaultState = new Waiting(origin, this);
                AddState("Waiting", defaultState);
                AddState("Pulling", new Pulling(origin, this));
            }
            public override void OnStateEnter()
            {
                base.OnStateEnter();
                origin.fishingRod.fishingPin.gameObject.SetActive(true);
                origin.wielder.movement.canMove = false;
            }
            public override void OnStateExit()
            {
                base.OnStateExit();
                origin.fishingRod.fishingPin.gameObject.SetActive(false);
                origin.wielder.movement.canMove = true;
            }
            class Waiting : State<FishingRodItem, FishingRodVals>
            {
                public Waiting(FishingRodItem origin, Layer<FishingRodItem, FishingRodVals> parent) : base(origin, parent)
                {

                }
                float waitTime = 0.0f;
                public override void OnStateEnter()
                {
                    base.OnStateEnter();
                    waitTime = UnityEngine.Random.Range(origin.data.minWaitTime, origin.data.maxWaitTime);
                }
                public override void OnStateUpdate()
                {
                    base.OnStateUpdate();
                    waitTime -= Time.deltaTime;
                    if(waitTime <= 0.0f)
                    {
                        parentLayer.ChangeState("Pulling");
                    }
                }
            }
            class Pulling : State<FishingRodItem, FishingRodVals>
            {
                public Pulling(FishingRodItem origin, Layer<FishingRodItem, FishingRodVals> parent) : base(origin, parent)
                {

                }
                int clickNeeded;
                const int minClickNeeded = 10, maxClickNeeded = 20;
                const float timeLimit = 6.0f;
                public override void OnStateEnter()
                {
                    base.OnStateEnter();
                    clickNeeded = UnityEngine.Random.Range(minClickNeeded, maxClickNeeded + 1);
                    clicked = 0;
                    counter = 0.0f;
                    origin.wielder.misc.fishIcon.gameObject.SetActive(true);
                }
                public override void OnStateExit()
                {
                    base.OnStateExit();
                    origin.wielder.misc.fishIcon.gameObject.SetActive(false);
                }
                int clicked = 0;
                float counter = 0.0f;
                public override void OnStateUpdate()
                {
                    base.OnStateUpdate();
                    counter += Time.deltaTime;
                    if(counter >= timeLimit)
                    {
                        parentLayer.parentLayer.ChangeState("Idle");
                        return;
                    }

                    if (Input.GetMouseButtonDown(0))
                    {
                        clicked++;
                        if (clicked >= clickNeeded)
                        {
                            origin.data.castCooldownSource.cooldown = origin.data.castCooldown;
                            origin.data.castCooldownSource.cooldownLeft = origin.data.castCooldown;
                            origin.wielder.cooldowns.AddCooldown(origin.data.castCooldownSource);
                            foreach (var i in origin.data.loot.GenerateLoot())
                            {
                                origin.wielder.inventory.Insert_DropRest(i.item, i.count);
                            }
                            origin.wielder.statistics.chillExperience += origin.data.fishExperience;
                            parentLayer.parentLayer.ChangeState("Idle");
                        }
                    }
                }
            }
        }
    }
    public IEnumerable<LangText> GetStats()
    {
        yield return new()
        {
            kr = $"쿨타임: {data.castCooldown}"
        };
    }
}
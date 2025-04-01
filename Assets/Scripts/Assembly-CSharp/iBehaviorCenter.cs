using System.Collections.Generic;
using BehaviorTree;

public class iBehaviorCenter
{
	protected Dictionary<int, Node> m_dictBehavior;

	public iBehaviorCenter()
	{
		m_dictBehavior = new Dictionary<int, Node>();
	}

	public void Load()
	{
		CreateBehavior0();
		CreateBehavior1();
		CreateBehavior2();
		CreateBehavior3();
		CreateBehavior4();
		CreateBehavior100();
		CreateBehavior101();
		CreateBehavior102();
		CreateBehavior104();
	}

	public Node GetBehavior(int nID)
	{
		if (!m_dictBehavior.ContainsKey(nID))
		{
			return null;
		}
		return m_dictBehavior[nID];
	}

	protected void CreateBehavior0()
	{
		CompositeNode_Sequence compositeNode_Sequence = new CompositeNode_Sequence();
		compositeNode_Sequence.AddChild(new lgHasDeadNode());
		compositeNode_Sequence.AddChild(new doDeadUserNode());
		CompositeNode_Sequence compositeNode_Sequence2 = new CompositeNode_Sequence();
		compositeNode_Sequence2.AddChild(new lgHasHurtNode());
		compositeNode_Sequence2.AddChild(new doHurtUserNode());
		CompositeNode_Sequence compositeNode_Sequence3 = new CompositeNode_Sequence();
		compositeNode_Sequence3.AddChild(new lgHasBeatBackNode());
		compositeNode_Sequence3.AddChild(new doBeatBackUserNode());
		CompositeNode_Sequence compositeNode_Sequence4 = new CompositeNode_Sequence();
		compositeNode_Sequence4.AddChild(new lgHasStunNode());
		compositeNode_Sequence4.AddChild(new doStunNode());
		CompositeNode_Selector compositeNode_Selector = new CompositeNode_Selector();
		compositeNode_Selector.AddChild(compositeNode_Sequence);
		compositeNode_Selector.AddChild(compositeNode_Sequence3);
		compositeNode_Selector.AddChild(compositeNode_Sequence4);
		compositeNode_Selector.AddChild(compositeNode_Sequence2);
		m_dictBehavior.Add(0, compositeNode_Selector);
	}

	protected void CreateBehavior1()
	{
		CompositeNode_Sequence compositeNode_Sequence = new CompositeNode_Sequence();
		compositeNode_Sequence.AddChild(new lgHasDeadNode());
		compositeNode_Sequence.AddChild(new doDeadNode());
		CompositeNode_Sequence compositeNode_Sequence2 = new CompositeNode_Sequence();
		compositeNode_Sequence2.AddChild(new lgHasBeatBackNode());
		compositeNode_Sequence2.AddChild(new doBeatBackNode());
		CompositeNode_Sequence compositeNode_Sequence3 = new CompositeNode_Sequence();
		compositeNode_Sequence3.AddChild(new lgHasHurtNode());
		compositeNode_Sequence3.AddChild(new doHurtNode());
		CompositeNode_Sequence compositeNode_Sequence4 = new CompositeNode_Sequence();
		compositeNode_Sequence4.AddChild(new lgIsMoribundNode());
		compositeNode_Sequence4.AddChild(new doMoribundNode());
		CompositeNode_Sequence compositeNode_Sequence5 = new CompositeNode_Sequence();
		compositeNode_Sequence5.AddChild(new lgHasStunNode());
		compositeNode_Sequence5.AddChild(new doStunNode());
		CompositeNode_Sequence compositeNode_Sequence6 = new CompositeNode_Sequence();
		compositeNode_Sequence6.AddChild(new lgIsBlackNode());
		compositeNode_Sequence6.AddChild(new doBlackNode());
		CompositeNode_Sequence compositeNode_Sequence7 = new CompositeNode_Sequence();
		compositeNode_Sequence7.AddChild(new lgIsFreezeNode());
		compositeNode_Sequence7.AddChild(new doFreezeNode());
		CompositeNode_Sequence compositeNode_Sequence8 = new CompositeNode_Sequence();
		compositeNode_Sequence8.AddChild(new lgCheckHeightNode());
		compositeNode_Sequence8.AddChild(new doRandomFlyHeightNode());
		compositeNode_Sequence8.AddChild(new doFlyHeightNode());
		CompositeNode_Sequence compositeNode_Sequence9 = new CompositeNode_Sequence();
		compositeNode_Sequence9.AddChild(new lgIsRoarNode(5f));
		compositeNode_Sequence9.AddChild(new doRoarNode());
		CompositeNode_Sequence compositeNode_Sequence10 = new CompositeNode_Sequence();
		compositeNode_Sequence10.AddChild(new doRandomPosNode());
		compositeNode_Sequence10.AddChild(new doMoveToNode());
		conRandomMainTimeNode conRandomMainTimeNode2 = new conRandomMainTimeNode(1f, 3f);
		conRandomMainTimeNode2.AddChild(new doMoveToNode());
		CompositeNode_Sequence compositeNode_Sequence11 = new CompositeNode_Sequence();
		compositeNode_Sequence11.AddChild(new doTrailTargetPosNode());
		compositeNode_Sequence11.AddChild(conRandomMainTimeNode2);
		ConditionNode_MaintainTimeNode conditionNode_MaintainTimeNode = new ConditionNode_MaintainTimeNode(2f);
		conditionNode_MaintainTimeNode.AddChild(new doIdleNode(2f));
		CompositeNode_Sequence compositeNode_Sequence12 = new CompositeNode_Sequence();
		compositeNode_Sequence12.AddChild(new lgIsTargetCloseNode());
		compositeNode_Sequence12.AddChild(new doRoarNode());
		CompositeNode_Sequence compositeNode_Sequence13 = new CompositeNode_Sequence();
		compositeNode_Sequence13.AddChild(new lgHasNoTargetNode());
		compositeNode_Sequence13.AddChild(compositeNode_Sequence10);
		CompositeNode_Sequence compositeNode_Sequence14 = new CompositeNode_Sequence();
		compositeNode_Sequence14.AddChild(compositeNode_Sequence11);
		CompositeNode_Selector compositeNode_Selector = new CompositeNode_Selector();
		compositeNode_Selector.AddChild(compositeNode_Sequence12);
		compositeNode_Selector.AddChild(compositeNode_Sequence13);
		compositeNode_Selector.AddChild(compositeNode_Sequence14);
		CompositeNode_Selector compositeNode_Selector2 = new CompositeNode_Selector();
		compositeNode_Selector2.AddChild(compositeNode_Sequence9);
		compositeNode_Selector2.AddChild(compositeNode_Selector);
		CompositeNode_Selector compositeNode_Selector3 = new CompositeNode_Selector();
		compositeNode_Selector3.AddChild(new lgHasTargetNode());
		compositeNode_Selector3.AddChild(new doSelectTargetNode());
		CompositeNode_Selector compositeNode_Selector4 = new CompositeNode_Selector();
		compositeNode_Selector4.AddChild(new lgHasSkillNode());
		compositeNode_Selector4.AddChild(new doSelectSkillNode());
		CompositeNode_Sequence compositeNode_Sequence15 = new CompositeNode_Sequence();
		compositeNode_Sequence15.AddChild(compositeNode_Selector3);
		compositeNode_Sequence15.AddChild(compositeNode_Selector4);
		compositeNode_Sequence15.AddChild(new doUseSkillNode());
		CompositeNode_Sequence compositeNode_Sequence16 = new CompositeNode_Sequence();
		compositeNode_Sequence16.AddChild(compositeNode_Selector3);
		compositeNode_Sequence16.AddChild(new lgHasShowTimeNode());
		compositeNode_Sequence16.AddChild(new doShowTimeNode());
		CompositeNode_Selector compositeNode_Selector5 = new CompositeNode_Selector();
		compositeNode_Selector5.AddChild(compositeNode_Sequence);
		compositeNode_Selector5.AddChild(compositeNode_Sequence4);
		compositeNode_Selector5.AddChild(compositeNode_Sequence2);
		compositeNode_Selector5.AddChild(compositeNode_Sequence3);
		compositeNode_Selector5.AddChild(compositeNode_Sequence5);
		compositeNode_Selector5.AddChild(compositeNode_Sequence8);
		compositeNode_Selector5.AddChild(compositeNode_Sequence16);
		compositeNode_Selector5.AddChild(compositeNode_Sequence6);
		compositeNode_Selector5.AddChild(compositeNode_Sequence7);
		compositeNode_Selector5.AddChild(compositeNode_Sequence15);
		compositeNode_Selector5.AddChild(compositeNode_Selector2);
		compositeNode_Selector5.AddChild(conditionNode_MaintainTimeNode);
		m_dictBehavior.Add(1, compositeNode_Selector5);
	}

	protected void CreateBehavior2()
	{
		CompositeNode_Sequence compositeNode_Sequence = new CompositeNode_Sequence();
		compositeNode_Sequence.AddChild(new lgHasDeadNode());
		compositeNode_Sequence.AddChild(new doDeadNode());
		CompositeNode_Sequence compositeNode_Sequence2 = new CompositeNode_Sequence();
		compositeNode_Sequence2.AddChild(new lgHasBeatBackNode());
		compositeNode_Sequence2.AddChild(new doBeatBackNode());
		CompositeNode_Sequence compositeNode_Sequence3 = new CompositeNode_Sequence();
		compositeNode_Sequence3.AddChild(new lgHasHurtNode());
		compositeNode_Sequence3.AddChild(new doHurtNode());
		CompositeNode_Sequence compositeNode_Sequence4 = new CompositeNode_Sequence();
		compositeNode_Sequence4.AddChild(new lgHasStunNode());
		compositeNode_Sequence4.AddChild(new doStunNode());
		CompositeNode_Sequence compositeNode_Sequence5 = new CompositeNode_Sequence();
		compositeNode_Sequence5.AddChild(new lgIsBlackNode());
		compositeNode_Sequence5.AddChild(new doBlackNode());
		CompositeNode_Sequence compositeNode_Sequence6 = new CompositeNode_Sequence();
		compositeNode_Sequence6.AddChild(new lgIsFreezeNode());
		compositeNode_Sequence6.AddChild(new doFreezeNode());
		ConditionNode_MaintainTimeNode conditionNode_MaintainTimeNode = new ConditionNode_MaintainTimeNode(2f);
		conditionNode_MaintainTimeNode.AddChild(new doIdleNode(2f));
		CompositeNode_Selector compositeNode_Selector = new CompositeNode_Selector();
		compositeNode_Selector.AddChild(new lgHasTargetNode());
		compositeNode_Selector.AddChild(new doSelectTargetNode());
		CompositeNode_Selector compositeNode_Selector2 = new CompositeNode_Selector();
		compositeNode_Selector2.AddChild(new lgHasSkillNode());
		compositeNode_Selector2.AddChild(new doSelectSkillNode());
		CompositeNode_Sequence compositeNode_Sequence7 = new CompositeNode_Sequence();
		compositeNode_Sequence7.AddChild(new doRandomHoverPosNode());
		compositeNode_Sequence7.AddChild(new doHoverToNode());
		CompositeNode_Sequence compositeNode_Sequence8 = new CompositeNode_Sequence();
		compositeNode_Sequence8.AddChild(new lgHasHoverTimeNode(5f));
		compositeNode_Sequence8.AddChild(new doWaitNode(1f));
		compositeNode_Sequence8.AddChild(new doSelectTargetNode());
		compositeNode_Sequence8.AddChild(new doSelectSkillNode());
		CompositeNode_Sequence compositeNode_Sequence9 = new CompositeNode_Sequence();
		compositeNode_Sequence9.AddChild(new lgHasTargetNode());
		compositeNode_Sequence9.AddChild(new lgHasSkillNode());
		compositeNode_Sequence9.AddChild(new doUseSkillNode());
		CompositeNode_ParallelOR compositeNode_ParallelOR = new CompositeNode_ParallelOR();
		compositeNode_ParallelOR.AddChild(compositeNode_Sequence7);
		compositeNode_ParallelOR.AddChild(compositeNode_Sequence8);
		CompositeNode_Sequence compositeNode_Sequence10 = new CompositeNode_Sequence();
		compositeNode_Sequence10.AddChild(compositeNode_Selector);
		compositeNode_Sequence10.AddChild(new lgHasShowTimeNode());
		compositeNode_Sequence10.AddChild(new doShowTimeNode());
		CompositeNode_Selector compositeNode_Selector3 = new CompositeNode_Selector();
		compositeNode_Selector3.AddChild(compositeNode_Sequence);
		compositeNode_Selector3.AddChild(compositeNode_Sequence2);
		compositeNode_Selector3.AddChild(compositeNode_Sequence3);
		compositeNode_Selector3.AddChild(compositeNode_Sequence4);
		compositeNode_Selector3.AddChild(compositeNode_Sequence10);
		compositeNode_Selector3.AddChild(compositeNode_Sequence5);
		compositeNode_Selector3.AddChild(compositeNode_Sequence6);
		compositeNode_Selector3.AddChild(compositeNode_Sequence9);
		compositeNode_Selector3.AddChild(compositeNode_ParallelOR);
		compositeNode_Selector3.AddChild(conditionNode_MaintainTimeNode);
		m_dictBehavior.Add(2, compositeNode_Selector3);
	}

	protected void CreateBehavior3()
	{
		CompositeNode_Sequence compositeNode_Sequence = new CompositeNode_Sequence();
		compositeNode_Sequence.AddChild(new lgHasDeadNode());
		compositeNode_Sequence.AddChild(new doDeadNode());
		CompositeNode_Sequence compositeNode_Sequence2 = new CompositeNode_Sequence();
		compositeNode_Sequence2.AddChild(new lgHasBeatBackNode());
		compositeNode_Sequence2.AddChild(new doBeatBackNode());
		CompositeNode_Sequence compositeNode_Sequence3 = new CompositeNode_Sequence();
		compositeNode_Sequence3.AddChild(new lgHasHurtNode());
		compositeNode_Sequence3.AddChild(new doHurtNode());
		CompositeNode_Sequence compositeNode_Sequence4 = new CompositeNode_Sequence();
		compositeNode_Sequence4.AddChild(new lgIsMoribundNode());
		compositeNode_Sequence4.AddChild(new doMoribundNode());
		CompositeNode_Sequence compositeNode_Sequence5 = new CompositeNode_Sequence();
		compositeNode_Sequence5.AddChild(new lgHasStunNode());
		compositeNode_Sequence5.AddChild(new doStunNode());
		CompositeNode_Sequence compositeNode_Sequence6 = new CompositeNode_Sequence();
		compositeNode_Sequence6.AddChild(new lgIsBlackNode());
		compositeNode_Sequence6.AddChild(new doBlackNode());
		CompositeNode_Sequence compositeNode_Sequence7 = new CompositeNode_Sequence();
		compositeNode_Sequence7.AddChild(new lgIsFreezeNode());
		compositeNode_Sequence7.AddChild(new doFreezeNode());
		CompositeNode_Sequence compositeNode_Sequence8 = new CompositeNode_Sequence();
		compositeNode_Sequence8.AddChild(new lgCheckHeightNode());
		compositeNode_Sequence8.AddChild(new doRandomFlyHeightNode());
		compositeNode_Sequence8.AddChild(new doFlyHeightNode());
		ConditionNode_MaintainTimeNode conditionNode_MaintainTimeNode = new ConditionNode_MaintainTimeNode(2f);
		conditionNode_MaintainTimeNode.AddChild(new doIdleNode(2f));
		CompositeNode_Sequence compositeNode_Sequence9 = new CompositeNode_Sequence();
		compositeNode_Sequence9.AddChild(new lgHasShowTimeNode());
		compositeNode_Sequence9.AddChild(new doShowTimeNode());
		CompositeNode_Selector compositeNode_Selector = new CompositeNode_Selector();
		compositeNode_Selector.AddChild(new lgHasPurposePointNode());
		compositeNode_Selector.AddChild(new doPurposePointNode());
		conRandomMainTimeNode conRandomMainTimeNode2 = new conRandomMainTimeNode(2f, 3f);
		conRandomMainTimeNode2.AddChild(new doMoveToNode());
		CompositeNode_Sequence compositeNode_Sequence10 = new CompositeNode_Sequence();
		compositeNode_Sequence10.AddChild(compositeNode_Selector);
		compositeNode_Sequence10.AddChild(conRandomMainTimeNode2);
		CompositeNode_Sequence compositeNode_Sequence11 = new CompositeNode_Sequence();
		CompositeNode_Selector compositeNode_Selector2 = new CompositeNode_Selector();
		compositeNode_Selector2.AddChild(new lgHasTargetBuildingNode());
		compositeNode_Selector2.AddChild(new doSelectTargetBuildingNode());
		CompositeNode_Sequence compositeNode_Sequence12 = new CompositeNode_Sequence();
		compositeNode_Sequence12.AddChild(new doMoveToNode());
		compositeNode_Sequence12.AddChild(new doAttackBuildingNode());
		compositeNode_Sequence11.AddChild(compositeNode_Selector2);
		compositeNode_Sequence11.AddChild(compositeNode_Sequence12);
		CompositeNode_Selector compositeNode_Selector3 = new CompositeNode_Selector();
		compositeNode_Selector3.AddChild(compositeNode_Sequence);
		compositeNode_Selector3.AddChild(compositeNode_Sequence4);
		compositeNode_Selector3.AddChild(compositeNode_Sequence2);
		compositeNode_Selector3.AddChild(compositeNode_Sequence3);
		compositeNode_Selector3.AddChild(compositeNode_Sequence5);
		compositeNode_Selector3.AddChild(compositeNode_Sequence8);
		compositeNode_Selector3.AddChild(compositeNode_Sequence9);
		compositeNode_Selector3.AddChild(compositeNode_Sequence6);
		compositeNode_Selector3.AddChild(compositeNode_Sequence7);
		compositeNode_Selector3.AddChild(compositeNode_Sequence11);
		compositeNode_Selector3.AddChild(compositeNode_Sequence10);
		compositeNode_Selector3.AddChild(conditionNode_MaintainTimeNode);
		m_dictBehavior.Add(3, compositeNode_Selector3);
	}

	protected void CreateBehavior4()
	{
		CompositeNode_Sequence compositeNode_Sequence = new CompositeNode_Sequence();
		compositeNode_Sequence.AddChild(new lgHasDeadNode());
		compositeNode_Sequence.AddChild(new doDeadNode());
		CompositeNode_Sequence compositeNode_Sequence2 = new CompositeNode_Sequence();
		compositeNode_Sequence2.AddChild(new lgHasBeatBackNode());
		compositeNode_Sequence2.AddChild(new doBeatBackNode());
		CompositeNode_Sequence compositeNode_Sequence3 = new CompositeNode_Sequence();
		compositeNode_Sequence3.AddChild(new lgHasHurtNode());
		compositeNode_Sequence3.AddChild(new doHurtNode());
		CompositeNode_Sequence compositeNode_Sequence4 = new CompositeNode_Sequence();
		compositeNode_Sequence4.AddChild(new lgIsMoribundNode());
		compositeNode_Sequence4.AddChild(new doMoribundNode());
		CompositeNode_Sequence compositeNode_Sequence5 = new CompositeNode_Sequence();
		compositeNode_Sequence5.AddChild(new lgHasStunNode());
		compositeNode_Sequence5.AddChild(new doStunNode());
		CompositeNode_Sequence compositeNode_Sequence6 = new CompositeNode_Sequence();
		compositeNode_Sequence6.AddChild(new lgIsBlackNode());
		compositeNode_Sequence6.AddChild(new doBlackNode());
		CompositeNode_Sequence compositeNode_Sequence7 = new CompositeNode_Sequence();
		compositeNode_Sequence7.AddChild(new lgIsFreezeNode());
		compositeNode_Sequence7.AddChild(new doFreezeNode());
		ConditionNode_MaintainTimeNode conditionNode_MaintainTimeNode = new ConditionNode_MaintainTimeNode(2f);
		conditionNode_MaintainTimeNode.AddChild(new doIdleNode(2f));
		CompositeNode_Sequence compositeNode_Sequence8 = new CompositeNode_Sequence();
		compositeNode_Sequence8.AddChild(new lgHasShowTimeNode());
		compositeNode_Sequence8.AddChild(new doShowTimeNode());
		CompositeNode_Selector compositeNode_Selector = new CompositeNode_Selector();
		compositeNode_Selector.AddChild(new lgHasFarestStartPointNode());
		compositeNode_Selector.AddChild(new doSelectFarestPointNode());
		CompositeNode_Sequence compositeNode_Sequence9 = new CompositeNode_Sequence();
		compositeNode_Sequence9.AddChild(compositeNode_Selector);
		compositeNode_Sequence9.AddChild(new doMoveToNode());
		compositeNode_Sequence9.AddChild(new doDisappearNode());
		CompositeNode_Selector compositeNode_Selector2 = new CompositeNode_Selector();
		compositeNode_Selector2.AddChild(compositeNode_Sequence);
		compositeNode_Selector2.AddChild(compositeNode_Sequence4);
		compositeNode_Selector2.AddChild(compositeNode_Sequence2);
		compositeNode_Selector2.AddChild(compositeNode_Sequence3);
		compositeNode_Selector2.AddChild(compositeNode_Sequence5);
		compositeNode_Selector2.AddChild(compositeNode_Sequence8);
		compositeNode_Selector2.AddChild(compositeNode_Sequence6);
		compositeNode_Selector2.AddChild(compositeNode_Sequence7);
		compositeNode_Selector2.AddChild(compositeNode_Sequence9);
		compositeNode_Selector2.AddChild(conditionNode_MaintainTimeNode);
		m_dictBehavior.Add(4, compositeNode_Selector2);
	}

	protected void CreateBehavior100()
	{
		CompositeNode_Sequence compositeNode_Sequence = new CompositeNode_Sequence();
		compositeNode_Sequence.AddChild(new lgHasDeadNode());
		compositeNode_Sequence.AddChild(new doDeadPlayerNode());
		CompositeNode_Sequence compositeNode_Sequence2 = new CompositeNode_Sequence();
		compositeNode_Sequence2.AddChild(new lgHasHurtNode());
		compositeNode_Sequence2.AddChild(new doHurtPlayerNode());
		CompositeNode_Sequence compositeNode_Sequence3 = new CompositeNode_Sequence();
		compositeNode_Sequence3.AddChild(new lgHasBeatBackNode());
		compositeNode_Sequence3.AddChild(new doBeatBackPlayerNode());
		CompositeNode_Sequence compositeNode_Sequence4 = new CompositeNode_Sequence();
		compositeNode_Sequence4.AddChild(new lgHasStunNode());
		compositeNode_Sequence4.AddChild(new doStunNode());
		CompositeNode_Parallel compositeNode_Parallel = new CompositeNode_Parallel();
		compositeNode_Parallel.AddChild(new doMoveToPlayerNode());
		compositeNode_Parallel.AddChild(new doAimToPlayerNode());
		CompositeNode_Sequence compositeNode_Sequence5 = new CompositeNode_Sequence();
		compositeNode_Sequence5.AddChild(new lgSkillPlayerNode());
		compositeNode_Sequence5.AddChild(new doSkillPlayerNode());
		CompositeNode_Sequence compositeNode_Sequence6 = new CompositeNode_Sequence();
		compositeNode_Sequence6.AddChild(new lgShootPlayerNode());
		compositeNode_Sequence6.AddChild(new doShootPlayerNode());
		CompositeNode_Selector compositeNode_Selector = new CompositeNode_Selector();
		compositeNode_Selector.AddChild(compositeNode_Sequence5);
		compositeNode_Selector.AddChild(compositeNode_Sequence6);
		CompositeNode_Parallel compositeNode_Parallel2 = new CompositeNode_Parallel();
		compositeNode_Parallel2.AddChild(compositeNode_Parallel);
		compositeNode_Parallel2.AddChild(compositeNode_Selector);
		CompositeNode_Selector compositeNode_Selector2 = new CompositeNode_Selector();
		compositeNode_Selector2.AddChild(compositeNode_Sequence);
		compositeNode_Selector2.AddChild(compositeNode_Sequence3);
		compositeNode_Selector2.AddChild(compositeNode_Sequence2);
		compositeNode_Selector2.AddChild(compositeNode_Sequence4);
		compositeNode_Selector2.AddChild(compositeNode_Parallel2);
		m_dictBehavior.Add(100, compositeNode_Selector2);
	}

	protected void CreateBehavior101()
	{
		CompositeNode_Sequence compositeNode_Sequence = new CompositeNode_Sequence();
		compositeNode_Sequence.AddChild(new lgHasDeadNode());
		compositeNode_Sequence.AddChild(new doDeadNode());
		CompositeNode_Sequence compositeNode_Sequence2 = new CompositeNode_Sequence();
		compositeNode_Sequence2.AddChild(new lgHasBeatBackNode());
		compositeNode_Sequence2.AddChild(new doBeatBackNode());
		CompositeNode_Sequence compositeNode_Sequence3 = new CompositeNode_Sequence();
		compositeNode_Sequence3.AddChild(new lgHasHurtNode());
		compositeNode_Sequence3.AddChild(new doHurtNode());
		CompositeNode_Sequence compositeNode_Sequence4 = new CompositeNode_Sequence();
		compositeNode_Sequence4.AddChild(new lgHasStunNode());
		compositeNode_Sequence4.AddChild(new doStunNode());
		CompositeNode_Sequence compositeNode_Sequence5 = new CompositeNode_Sequence();
		compositeNode_Sequence5.AddChild(new lgIsBlackNode());
		compositeNode_Sequence5.AddChild(new doBlackNode());
		CompositeNode_Sequence compositeNode_Sequence6 = new CompositeNode_Sequence();
		compositeNode_Sequence6.AddChild(new lgCheckHeightNode());
		compositeNode_Sequence6.AddChild(new doRandomFlyHeightNode());
		compositeNode_Sequence6.AddChild(new doFlyHeightNode());
		CompositeNode_Sequence compositeNode_Sequence7 = new CompositeNode_Sequence();
		compositeNode_Sequence7.AddChild(new lgHasShowTimeNode());
		compositeNode_Sequence7.AddChild(new doShowTimeNode());
		CompositeNode_Sequence compositeNode_Sequence8 = new CompositeNode_Sequence();
		compositeNode_Sequence8.AddChild(new lgHasActionNode());
		compositeNode_Sequence8.AddChild(new doActionNode());
		CompositeNode_Sequence compositeNode_Sequence9 = new CompositeNode_Sequence();
		compositeNode_Sequence9.AddChild(new lgHasTargetNode());
		compositeNode_Sequence9.AddChild(new lgHasSkillNode());
		compositeNode_Sequence9.AddChild(new doUseSkillNode());
		CompositeNode_Sequence compositeNode_Sequence10 = new CompositeNode_Sequence();
		compositeNode_Sequence10.AddChild(new lgHasPathNode());
		compositeNode_Sequence10.AddChild(new doMoveToNode());
		CompositeNode_Selector compositeNode_Selector = new CompositeNode_Selector();
		compositeNode_Selector.AddChild(compositeNode_Sequence);
		compositeNode_Selector.AddChild(compositeNode_Sequence2);
		compositeNode_Selector.AddChild(compositeNode_Sequence5);
		compositeNode_Selector.AddChild(compositeNode_Sequence3);
		compositeNode_Selector.AddChild(compositeNode_Sequence4);
		compositeNode_Selector.AddChild(compositeNode_Sequence7);
		compositeNode_Selector.AddChild(compositeNode_Sequence8);
		compositeNode_Selector.AddChild(compositeNode_Sequence9);
		compositeNode_Selector.AddChild(compositeNode_Sequence10);
		compositeNode_Selector.AddChild(new doIdleNode(2f));
		m_dictBehavior.Add(101, compositeNode_Selector);
	}

	protected void CreateBehavior102()
	{
		CompositeNode_Sequence compositeNode_Sequence = new CompositeNode_Sequence();
		compositeNode_Sequence.AddChild(new lgHasDeadNode());
		compositeNode_Sequence.AddChild(new doDeadNode());
		CompositeNode_Sequence compositeNode_Sequence2 = new CompositeNode_Sequence();
		compositeNode_Sequence2.AddChild(new lgHasBeatBackNode());
		compositeNode_Sequence2.AddChild(new doBeatBackNode());
		CompositeNode_Sequence compositeNode_Sequence3 = new CompositeNode_Sequence();
		compositeNode_Sequence3.AddChild(new lgHasHurtNode());
		compositeNode_Sequence3.AddChild(new doHurtNode());
		CompositeNode_Sequence compositeNode_Sequence4 = new CompositeNode_Sequence();
		compositeNode_Sequence4.AddChild(new lgHasStunNode());
		compositeNode_Sequence4.AddChild(new doStunNode());
		CompositeNode_Sequence compositeNode_Sequence5 = new CompositeNode_Sequence();
		compositeNode_Sequence5.AddChild(new lgIsBlackNode());
		compositeNode_Sequence5.AddChild(new doBlackNode());
		CompositeNode_Sequence compositeNode_Sequence6 = new CompositeNode_Sequence();
		compositeNode_Sequence6.AddChild(new lgHasShowTimeNode());
		compositeNode_Sequence6.AddChild(new doShowTimeNode());
		CompositeNode_Sequence compositeNode_Sequence7 = new CompositeNode_Sequence();
		compositeNode_Sequence7.AddChild(new lgHasActionNode());
		compositeNode_Sequence7.AddChild(new doActionNode());
		CompositeNode_Sequence compositeNode_Sequence8 = new CompositeNode_Sequence();
		compositeNode_Sequence8.AddChild(new lgHasTargetNode());
		compositeNode_Sequence8.AddChild(new lgHasSkillNode());
		compositeNode_Sequence8.AddChild(new doUseSkillNode());
		CompositeNode_Sequence compositeNode_Sequence9 = new CompositeNode_Sequence();
		compositeNode_Sequence9.AddChild(new lgHasHoverPathNode());
		compositeNode_Sequence9.AddChild(new doHoverToNode());
		CompositeNode_Selector compositeNode_Selector = new CompositeNode_Selector();
		compositeNode_Selector.AddChild(compositeNode_Sequence);
		compositeNode_Selector.AddChild(compositeNode_Sequence2);
		compositeNode_Selector.AddChild(compositeNode_Sequence5);
		compositeNode_Selector.AddChild(compositeNode_Sequence3);
		compositeNode_Selector.AddChild(compositeNode_Sequence4);
		compositeNode_Selector.AddChild(compositeNode_Sequence6);
		compositeNode_Selector.AddChild(compositeNode_Sequence7);
		compositeNode_Selector.AddChild(compositeNode_Sequence8);
		compositeNode_Selector.AddChild(compositeNode_Sequence9);
		compositeNode_Selector.AddChild(new doIdleNode(2f));
		m_dictBehavior.Add(102, compositeNode_Selector);
	}

	protected void CreateBehavior104()
	{
		CompositeNode_Sequence compositeNode_Sequence = new CompositeNode_Sequence();
		compositeNode_Sequence.AddChild(new lgHasDeadNode());
		compositeNode_Sequence.AddChild(new doDeadNode());
		CompositeNode_Sequence compositeNode_Sequence2 = new CompositeNode_Sequence();
		compositeNode_Sequence2.AddChild(new lgHasBeatBackNode());
		compositeNode_Sequence2.AddChild(new doBeatBackNode());
		CompositeNode_Sequence compositeNode_Sequence3 = new CompositeNode_Sequence();
		compositeNode_Sequence3.AddChild(new lgHasHurtNode());
		compositeNode_Sequence3.AddChild(new doHurtNode());
		CompositeNode_Sequence compositeNode_Sequence4 = new CompositeNode_Sequence();
		compositeNode_Sequence4.AddChild(new lgIsBlackNode());
		compositeNode_Sequence4.AddChild(new doBlackNode());
		CompositeNode_Sequence compositeNode_Sequence5 = new CompositeNode_Sequence();
		compositeNode_Sequence5.AddChild(new lgIsMoribundNode());
		compositeNode_Sequence5.AddChild(new doMoribundNode());
		CompositeNode_Sequence compositeNode_Sequence6 = new CompositeNode_Sequence();
		compositeNode_Sequence6.AddChild(new lgHasStunNode());
		compositeNode_Sequence6.AddChild(new doStunNode());
		ConditionNode_MaintainTimeNode conditionNode_MaintainTimeNode = new ConditionNode_MaintainTimeNode(2f);
		conditionNode_MaintainTimeNode.AddChild(new doIdleNode(2f));
		CompositeNode_Sequence compositeNode_Sequence7 = new CompositeNode_Sequence();
		compositeNode_Sequence7.AddChild(new lgHasShowTimeNode());
		compositeNode_Sequence7.AddChild(new doShowTimeNode());
		CompositeNode_Sequence compositeNode_Sequence8 = new CompositeNode_Sequence();
		compositeNode_Sequence8.AddChild(new lgHasActionNode());
		compositeNode_Sequence8.AddChild(new doActionNode());
		CompositeNode_Selector compositeNode_Selector = new CompositeNode_Selector();
		compositeNode_Selector.AddChild(new lgHasFarestStartPointNode());
		compositeNode_Selector.AddChild(new doSelectFarestPointNode());
		CompositeNode_Sequence compositeNode_Sequence9 = new CompositeNode_Sequence();
		compositeNode_Sequence9.AddChild(compositeNode_Selector);
		compositeNode_Sequence9.AddChild(new doMoveToNode());
		compositeNode_Sequence9.AddChild(new doDisappearNode());
		CompositeNode_Selector compositeNode_Selector2 = new CompositeNode_Selector();
		compositeNode_Selector2.AddChild(compositeNode_Sequence);
		compositeNode_Selector2.AddChild(compositeNode_Sequence5);
		compositeNode_Selector2.AddChild(compositeNode_Sequence2);
		compositeNode_Selector2.AddChild(compositeNode_Sequence3);
		compositeNode_Selector2.AddChild(compositeNode_Sequence6);
		compositeNode_Selector2.AddChild(compositeNode_Sequence7);
		compositeNode_Selector2.AddChild(compositeNode_Sequence4);
		compositeNode_Selector2.AddChild(compositeNode_Sequence8);
		compositeNode_Selector2.AddChild(compositeNode_Sequence9);
		compositeNode_Selector2.AddChild(conditionNode_MaintainTimeNode);
		m_dictBehavior.Add(104, compositeNode_Selector2);
	}
}
